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
    recyclingCenters: RecyclingCenter[] = []

    constructor(private service: RecyclingCenterService) {}

    ngOnInit(): void {
        this.service.getRecyclingCenters().subscribe(result => {
            this.recyclingCenters = result;
        }, error => console.error(error));
    }
}
