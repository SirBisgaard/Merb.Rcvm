import { Component } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

import { VehicleService } from "../../../services/vehicleservice";
import { RecyclingCenterService } from "../../../services/recyclingcenterservice";
import { SkatService } from "../../../services/skatservice";

import { IVehicle } from "../../../domain/Vehicle";
import { RecyclingCenter } from "../../../domain/RecyclingCenter";

@Component({
    selector: "vehicle-create-update",
    templateUrl: "./vehiclescreateupdate.component.html",
    styleUrls: ["./vehiclescreateupdate.component.css"],
    providers: [VehicleService, RecyclingCenterService, SkatService]
})
export class VehiclesCreateUpdateComponent {

    isUpdate: boolean = false;
    vehicle = {
        id: "",
        vinNumber: "",
        registrationNumber: "",
    } as IVehicle;

    recyclingCenters: RecyclingCenter[] = [];

    constructor(
        private readonly service: VehicleService,
        private readonly recyclingCenterService: RecyclingCenterService,
        private readonly skatService: SkatService,
        private readonly router: Router,
        private readonly activatedRoute: ActivatedRoute) { }

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

    getDataFromSkat(): void {
        this.skatService.getVehicle(this.vehicle.registrationNumber, this.vehicle.vinNumber).subscribe(result => {
            var update = (target: any, value: any): any => {
                if (value == undefined)
                    return target;
                if (value === "")
                    return target;
                return value;
            }

            this.vehicle.registrationNumber = update(this.vehicle.registrationNumber, result.registrationNumber);
            this.vehicle.vinNumber = update(this.vehicle.vinNumber, result.vinNumber);
            this.vehicle.brand = update(this.vehicle.brand, result.brand);
            this.vehicle.model = update(this.vehicle.model, result.model);
            this.vehicle.variant = update(this.vehicle.variant, result.variant);
        });
    }

    delete(): void {
        this.service.deleteVehicle(this.vehicle.id, () => {
            this.router.navigate(["vehicles"]);
        });
    }

    today(): string {
        const today = new Date();
        const dd = today.getDate();
        const mm = today.getMonth() + 1; //January is 0!
        const yyyy = today.getFullYear();

        return yyyy + '-' + mm + '-' + dd;
    }
}
