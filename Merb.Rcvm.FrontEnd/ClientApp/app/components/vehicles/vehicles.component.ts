import { Component } from "@angular/core";

import { VehicleService } from "../../services/vehicleservice";
import { RecyclingCenterService} from "../../services/recyclingcenterservice";
import { IVehicle } from "../../domain/Vehicle";

@Component({
    selector: "vehicles",
    templateUrl: "./vehicles.component.html",
    styleUrls: ["./vehicles.component.css"],
    providers: [VehicleService, RecyclingCenterService]
})
export class VehiclesComponent {
    title: string = "Vehicles";
    vehicles: IVehicle[] = [];

    constructor(private readonly vehicleService: VehicleService, private readonly recyclingCenterService: RecyclingCenterService) { }

    ngOnInit(): void {
    }

    recyclingCenterChange(id: string) {
        this.vehicleService.getVehicles(id).subscribe(result => {
            this.vehicles = result; 
        }, error => console.error(error));
    }

    delete(id: string): void {
        this.vehicleService.deleteVehicle(id, () => {
            this.ngOnInit();
        });
    }
}