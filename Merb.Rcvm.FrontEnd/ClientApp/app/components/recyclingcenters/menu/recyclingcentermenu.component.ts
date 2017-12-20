import { Component, Input, OnInit } from '@angular/core';

import { RecyclingCenterService } from "../../../services/recyclingcenterservice";
import { RecyclingCenter } from "../../../domain/RecyclingCenter";

@Component({
    selector: 'recycling-center-menu',
    templateUrl: './recyclingcentermenu.component.html',
    styleUrls: ['./recyclingcentermenu.component.css'],
    providers: [RecyclingCenterService]
})
export class RecyclingCenterMenuComponent implements OnInit {

    @Input()
    public title: string = "";
    public recyclingCenters: RecyclingCenter[] = []
    public selectedRecyclingCenter: string;

    constructor(private service: RecyclingCenterService) { }

    ngOnInit(): void {
        this.service.getRecyclingCenters().subscribe(result => {
            this.recyclingCenters = result;
            var selectedCenter = this.service.getSelectedRecyclingCenter()

            if (selectedCenter == undefined && this.recyclingCenters.length > 0)
                this.selectedRecyclingCenter = this.recyclingCenters[0].id;
            else
                this.selectedRecyclingCenter = selectedCenter.id;
        }, error => console.error(error));
    }

    public onChange(id: string): void {
        console.log(id)
        this.selectedRecyclingCenter = id;
        var center = this.recyclingCenters.find(center => center.id == id) as RecyclingCenter;
        this.service.setSelectedRecyclingCenter(center);
    }
}
