﻿import { Injectable, Inject } from "@angular/core";
import { Http } from '@angular/http';

import { RecyclingCenter } from "../domain/RecyclingCenter";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';

@Injectable()
export class RecyclingCenterService {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {}

    static selectedRecyclingCenter: RecyclingCenter

    public getRecyclingCenters(): Observable<RecyclingCenter[]> {
        return this.http.get(this.baseUrl + 'api/RecyclingCenter').map(res => (res.json() as RecyclingCenter[]));
    }

    public getSelectedRecyclingCenter(): RecyclingCenter {
        return RecyclingCenterService.selectedRecyclingCenter;
    }

    public setSelectedRecyclingCenter(recyclingCenter: RecyclingCenter): void {
        RecyclingCenterService.selectedRecyclingCenter = recyclingCenter;
    }
}