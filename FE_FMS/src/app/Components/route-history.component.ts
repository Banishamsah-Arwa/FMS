import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { VehicleService } from '../Services/vehicles.service';
import { RouteHistoryService } from '../Services/routehstory.services';

@Component({
  selector: 'app-add-route-history',
  templateUrl: './route-history.component.html',
  styleUrls: ['./route-history.component.css']
})
export class RouteHistoryComponent implements OnInit {
  vehicles: any[]=[];
  selectedVehicleID: any; 
  selectedVehicleHistory: any[] = [];
  constructor(private vehicleService: VehicleService, private routehistory: RouteHistoryService) { }

  ngOnInit(): void {
    this.loadVehicles();
  }

  loadVehicles(): void {
    console.log('Loading vehicles');
    this.vehicleService.getAllVehicles().subscribe((data: any) => {
      console.log('API Response:', data);
      if (data && data['Vehicles Information']) {
        this.vehicles = data['Vehicles Information'];
        console.log('Vehicles Array:', this.vehicles);
      } else {
        console.error('No vehicles information found in the response');
      }
    }, (error: any) => {
      console.error('Error fetching vehicles:', error);
    });
  }

  addRouteHistory(form: NgForm): void {
    const routeHistory = {
      vehicleID: form.value.vehicleID,
      vehicleDirection: form.value.vehicleDirection,
      status: form.value.status,
      vehicleSpeed: form.value.vehicleSpeed,
      epoch: form.value.epoch,
      address: form.value.address,
      latitude: form.value.latitude,
      longitude: form.value.longitude
    };

    this.routehistory.addRouteHistory(routeHistory).subscribe(
        (      response: any) => {
        console.log('Route history added successfully', response);
        form.reset();
      },
        (      error: any) => {
        console.error('Error adding route history', error);
      }
    );
  }

  onVehicleChange(): void {
    this.routehistory.getRouteHistoryId(this.selectedVehicleID).subscribe(
      (data: any) => {
        if (data && data['RouteHistory']) {
          this.selectedVehicleHistory = data.RouteHistory;
          console.log('RouteHistory Array:', this.selectedVehicleHistory);
        } else {
          console.error('No RouteHistory in the response');
        }
      },
      error => {
        console.error('Error fetching RouteHistory:', error);
      }
    );
  }
}
