import { Injectable, Inject } from "@angular/core";
import { Http, RequestOptions, Headers } from "@angular/http";

import { IVehicle } from "../domain/Vehicle";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";

@Injectable()
export class VehicleService {
    constructor(private readonly  http: Http, @Inject("BASE_URL") private baseUrl: string) { }

    getVehicles(id: string): Observable<IVehicle[]> {
        return this.http.get(this.baseUrl + "api/Vehicle/RecyclingCenter/" + id).map(res => {
            var v = (res.json() as IVehicle[]);
            for (let i = 0; i < v.length; i++) {
                v[i].scrappedDate = v[i].scrappedDate.substring(0, 10);
                v[i].environmentTreatmentDate = v[i].environmentTreatmentDate.substring(0, 10);
            }
            return v;
        });
    }

    getVehicle(id: string): Observable<IVehicle> {
        return this.http.get(this.baseUrl + "api/Vehicle/" + id).map(res => {
            var v = (res.json() as IVehicle);
            v.scrappedDate = v.scrappedDate.substring(0, 10);
            v.environmentTreatmentDate = v.environmentTreatmentDate.substring(0, 10);
            return v;
        });
    }

    deleteVehicle(id: string, complete: any): void {
        this.http.delete(this.baseUrl + "api/Vehicle/" + id).subscribe(complete, complete);
    }

    createVehicle(vehicle: IVehicle, complete: any): void {
        const url = this.baseUrl + "api/Vehicle";
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        const options = new RequestOptions({ headers: headers });

        this.http.post(url, JSON.stringify(vehicle), options).subscribe(complete, complete);
    }

    updateVehicle(vehicle: IVehicle, complete: any): void {
        const url = this.baseUrl + "api/Vehicle/" + vehicle.id;
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        const options = new RequestOptions({ headers: headers });

        this.http.put(url, JSON.stringify(vehicle), options).subscribe(complete, complete);
    }
}