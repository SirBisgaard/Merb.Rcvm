import { Component, OnInit } from "@angular/core";

import { RecyclingCenterService } from "../../../services/recyclingcenterservice";
import { RecyclingCenter } from "../../../domain/RecyclingCenter";

@Component({
    selector: "recycling-center-menu",
    templateUrl: "./recyclingcentermenu.component.html",
    styleUrls: ["./recyclingcentermenu.component.css"],
    providers: [RecyclingCenterService]
})
export class RecyclingCenterMenuComponent implements OnInit {
    public recyclingCenters: RecyclingCenter[] = []
    public selectedRecyclingCenter: string;

    constructor(private service: RecyclingCenterService) { }

    ngOnInit(): void {
        this.service.getRecyclingCenters().subscribe(result => {
            this.recyclingCenters = result;
            var selectedCenter = this.service.getSelectedRecyclingCenter()

            if (this.recyclingCenters.length == 0) {
                this.recyclingCenters = [{
                    id: "None",
                    name: "None"
                } as RecyclingCenter]
            }

            this.selectedRecyclingCenter = selectedCenter == undefined ?
                this.recyclingCenters[0].id :
                selectedCenter.id;
        }, error => console.error(error));
    }

    public onChange(id: string): void {
        this.selectedRecyclingCenter = id;
        var center = this.recyclingCenters.find(center => center.id == id) as RecyclingCenter;
        this.service.setSelectedRecyclingCenter(center);
    }
}
