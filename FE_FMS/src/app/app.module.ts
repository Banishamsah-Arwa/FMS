import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { VehicleListComponent } from './Components/all-vehicles.component';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common'; 
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VehicleService } from './Services/vehicles.service';
import { DriversService } from './Services/driver.service';
import { GeofencesComponent } from './Components/geofences.component';
import { VehicleDetailComponent } from './Components/vehicle-details.component';
import { GeofencesService } from './Services/geofences.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { DriverOptionComponent } from './Components/driver-options.component';
import { RouteHistoryComponent } from './Components/route-history.component';
import { InformationComponent } from './Components/information.component';
import { RouteHistoryService } from './Services/routehstory.services';
import { InformationService } from './Services/vehicleinformation.service';
import { VehicleComponent } from './Components/vehicle.component';

@NgModule({
  declarations: [
    AppComponent,
    VehicleListComponent,
    GeofencesComponent,
    VehicleDetailComponent,
    DriverOptionComponent,
    RouteHistoryComponent,
    InformationComponent,
    VehicleComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    BrowserAnimationsModule
  ],
  providers: [
    VehicleService,
    DriversService,
    GeofencesService,
    RouteHistoryService,
    InformationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
