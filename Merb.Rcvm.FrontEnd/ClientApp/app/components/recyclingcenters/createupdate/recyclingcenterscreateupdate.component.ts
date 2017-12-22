import { Component } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

import { RecyclingCenterService } from "../../../services/recyclingcenterservice";
import { RecyclingCenter } from "../../../domain/RecyclingCenter";

@Component({
    selector: "recycling-centers-create-update",
    templateUrl: "./recyclingcenterscreateupdate.component.html",
    styleUrls: ["./recyclingcenterscreateupdate.component.css"],
    providers: [RecyclingCenterService]
})
export class RecyclingCentersCreateUpdateComponent {

    public isUpdate: boolean = false;

    public recyclingCenter: RecyclingCenter = {
        id: "",
        name: "",
        email: "",
        phone: "",
        address: "",
        zip: "",
        city: ""
    } as RecyclingCenter;

    constructor(private service: RecyclingCenterService, private router: Router, private activatedRoute: ActivatedRoute) {  }

    ngOnInit(): void {
        this.activatedRoute.params.subscribe(params => {
            let id = params["id"];

            if (id == undefined)
                return;

            this.service.getRecyclingCenter(id).subscribe(result => {
                this.recyclingCenter = result;
                this.isUpdate = true;
            });
        });
    }

    public submit(): void {
        if (this.isUpdate) {
            this.service.updateRecyclingCenter(this.recyclingCenter, (result: any) => {
                this.router.navigate(["recycling-centers"]);
            });
        }
        else {
            this.service.createRecyclingCenter(this.recyclingCenter, (result: any) => {
                this.router.navigate(["recycling-centers"]);
            });
        }
    }

    public delete(): void {
        this.service.deleteRecyclingCenter(this.recyclingCenter.id, (result: any) => {
            this.router.navigate(["recycling-centers"]);
        });
    }
}
