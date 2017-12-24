import { Component } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

import { VehicleService } from "../../../services/vehicleservice";
import { RecyclingCenterService } from "../../../services/recyclingcenterservice";

import { IVehicle } from "../../../domain/Vehicle";
import { RecyclingCenter } from "../../../domain/RecyclingCenter";

@Component({
    selector: "vehicle-create-update",
    templateUrl: "./vehiclescreateupdate.component.html",
    styleUrls: ["./vehiclescreateupdate.component.css"],
    providers: [VehicleService, RecyclingCenterService]
})
export class VehiclesCreateUpdateComponent {

    isUpdate: boolean = false;
    vehicle = {
        id: "",
    } as IVehicle;

    recyclingCenters: RecyclingCenter[] = [];

    constructor(private readonly service: VehicleService, private readonly recyclingCenterService: RecyclingCenterService, private readonly router: Router, private readonly activatedRoute: ActivatedRoute) {  }

    ngOnInit(): void {
        this.activatedRoute.params.subscribe(params => {
            const id = params["id"];

            if (id == undefined)
                return;

            this.service.getVehicle(id).subscribe(result => {
                this.vehicle = result;
                this.isUpdate = true;
            });
        });

        this.recyclingCenterService.getRecyclingCenters().subscribe(result => {
            this.recyclingCenters = result;
            var selectedCenter = this.recyclingCenterService.getSelectedRecyclingCenter();

            if (this.recyclingCenters.length == 0) {
                this.recyclingCenters = [{
                    id: "None",
                    name: "None"
                } as RecyclingCenter];
            }

            this.vehicle.recyclingCenterId = selectedCenter == undefined ?
                this.recyclingCenters[0].id :
                selectedCenter.id;
        }, error => console.error(error));
    }

    submit(): void {
        if (this.isUpdate) {
            this.service.updateVehicle(this.vehicle, () => {
                this.router.navigate(["vehicles"]);
            });
        }
        else {
            this.service.createVehicle(this.vehicle, () => {
                this.router.navigate(["vehicles"]);
            });
        }
    }

    delete(): void {
        this.service.deleteVehicle(this.vehicle.id, () => {
            this.router.navigate(["vehicles"]);
        });
    }
}
