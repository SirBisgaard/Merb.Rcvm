import { Injectable, Inject } from "@angular/core";
import { Http, RequestOptions, Headers } from "@angular/http";

import { RecyclingCenter } from "../domain/RecyclingCenter";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";

@Injectable()
export class RecyclingCenterService {
    constructor(private http: Http, @Inject("BASE_URL") private baseUrl: string) { }

    static selectedRecyclingCenter: RecyclingCenter;

    public getRecyclingCenters(): Observable<RecyclingCenter[]> {
        return this.http.get(this.baseUrl + "api/RecyclingCenter").map(res => (res.json() as RecyclingCenter[]));
    }

    public getRecyclingCenter(id: string): Observable<RecyclingCenter> {
        return this.http.get(this.baseUrl + "api/RecyclingCenter/" + id).map(res => (res.json() as RecyclingCenter));
    }

    public deleteRecyclingCenter(id: string, complete: any): void {
        this.http.delete(this.baseUrl + "api/RecyclingCenter/" + id).subscribe(complete, complete);
    }

    public createRecyclingCenter(recyclingCenter: RecyclingCenter, complete: any): void {
        const url = this.baseUrl + "api/RecyclingCenter";
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        const options = new RequestOptions({ headers: headers });

        this.http.post(url, JSON.stringify(recyclingCenter), options).subscribe(complete, complete);
    }

    public updateRecyclingCenter(recyclingCenter: RecyclingCenter, complete: any): void {
        const url = this.baseUrl + "api/RecyclingCenter/" + recyclingCenter.id;
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        const options = new RequestOptions({ headers: headers });

        this.http.put(url, JSON.stringify(recyclingCenter), options).subscribe(complete, complete);
    }

    public getSelectedRecyclingCenter(): RecyclingCenter {
        return RecyclingCenterService.selectedRecyclingCenter;
    }

    public setSelectedRecyclingCenter(recyclingCenter: RecyclingCenter): void {
        RecyclingCenterService.selectedRecyclingCenter = recyclingCenter;
    }
}