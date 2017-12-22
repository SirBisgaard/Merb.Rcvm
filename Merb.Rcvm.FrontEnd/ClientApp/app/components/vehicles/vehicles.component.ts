import { Component } from "@angular/core";

import { RecyclingCenterService } from "../../services/recyclingcenterservice";
import { RecyclingCenter } from "../../domain/RecyclingCenter";

@Component({
    selector: "vehicles",
    templateUrl: "./vehicles.component.html",
    styleUrls: ["./vehicles.component.css"],
    providers: [RecyclingCenterService]
})
export class VehiclesComponent {
    title: string = "Vehicles";
    recyclingCenters: RecyclingCenter[] = [];

    constructor(private readonly service: RecyclingCenterService) { }

    ngOnInit(): void {
        this.service.getRecyclingCenters().subscribe(result => {
            this.recyclingCenters = result;
        }, error => console.error(error));
    }

    delete(id: string): void {
        this.service.deleteRecyclingCenter(id, (result: any) => {
            this.ngOnInit();
        });
    }
}