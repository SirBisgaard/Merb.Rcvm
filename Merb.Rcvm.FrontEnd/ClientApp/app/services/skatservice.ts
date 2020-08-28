import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";

import { IVehicle } from "../domain/Vehicle";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";

@Injectable()
export class SkatService {
    constructor(private readonly http: Http, @Inject("BASE_URL") private readonly baseUrl: string) { }

    getVehicle(registrationNumber: string, vin: string): Observable<IVehicle> {
        return this.http.get(this.baseUrl + "api/skat?registrationNumber=" + registrationNumber + "&vin=" + vin).map(res => (res.json() as IVehicle));
    }
}