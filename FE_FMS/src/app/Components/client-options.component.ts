import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../Services/vehicles.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './client-option.component.html',
  styleUrls: ['./client-option.component.css']
})
export class ClientOptionsComponent implements OnInit {
  

  constructor( private router: Router) { }

  ngOnInit(): void {
  }


  DriversManegment(): void {
    this.router.navigate(['/driver']);

  }
  VehiclManegment(): void {
    this.router.navigate(['/vehicleoptions']);

  }
  VehicleInformationManegment(): void {
    this.router.navigate(['/information']);

  }
  RouteHistoryManegment(): void {
    this.router.navigate(['/routehistory']);

  }
  GeofencesManegment(): void {
    this.router.navigate(['/geofences']);

  }
}
