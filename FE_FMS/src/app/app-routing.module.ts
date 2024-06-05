import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VehicleListComponent } from './Components/all-vehicles.component';
import { VehicleDetailComponent } from './Components/vehicle-details.component';
import { GeofencesComponent } from './Components/geofences.component';
import { DriverOptionComponent } from './Components/driver-options.component';
import { RouteHistoryComponent } from './Components/route-history.component';
import { InformationComponent } from './Components/information.component';
import { VehicleComponent } from './Components/vehicle.component';
import { ClientOptionsComponent } from './Components/client-options.component';


const routes: Routes = [
  { path: 'vehicles', component: VehicleListComponent },
  { path: 'geofences', component: GeofencesComponent },
  { path: 'driver', component: DriverOptionComponent },
  { path: 'routehistory', component: RouteHistoryComponent },
  { path: 'vehicleoptions', component: VehicleComponent },
  { path: 'home', component: ClientOptionsComponent },

  { path: 'information', component: InformationComponent },
  { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
  { path: 'vehicle/:id', component: VehicleDetailComponent },

];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
