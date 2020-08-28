import { Component } from "@angular/core";

import { RecyclingCenterService } from "../../services/recyclingcenterservice";
import { RecyclingCenter } from "../../domain/RecyclingCenter";

@Component({
    selector: "recycling-centers",
    templateUrl: "./recyclingcenters.component.html",
    styleUrls: ["./recyclingcenters.component.css"],
    providers: [RecyclingCenterService]
})
export class RecyclingCentersComponent {
    public title: string = "Recycling Centers";
    public recyclingCenters: RecyclingCenter[] = [];

    constructor(private service: RecyclingCenterService) { }

    ngOnInit(): void {
        this.service.getRecyclingCenters().subscribe(result => {
            this.recyclingCenters = result;
        }, error => console.error(error));
    }

    public delete(id: string): void {
        this.service.deleteRecyclingCenter(id, (result: any) => {
            this.ngOnInit();
        });
    }
}
