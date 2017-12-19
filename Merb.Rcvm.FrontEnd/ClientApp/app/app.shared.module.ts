import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { PartsComponent } from './components/parts/parts.component';
import { RecyclingCentersComponent } from './components/recyclingcenters/recyclingcenters.component';
import { ReportsComponent } from './components/reports/reports.component';
import { UsersComponent } from './components/users/users.component';
import { VehiclesComponent } from './components/vehicles/vehicles.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        DashboardComponent,
        PartsComponent,
        RecyclingCentersComponent,
        ReportsComponent,
        UsersComponent,
        VehiclesComponent,
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            { path: 'dashboard', component: DashboardComponent },
            { path: 'recycling-centers', component: RecyclingCentersComponent },
            { path: 'vehicles', component: VehiclesComponent },
            { path: 'parts', component: PartsComponent },
            { path: 'reports', component: ReportsComponent },
            { path: 'users', component: UsersComponent },
            { path: '**', redirectTo: 'dashboard' }
        ])
    ]
})
export class AppModuleShared {
}
