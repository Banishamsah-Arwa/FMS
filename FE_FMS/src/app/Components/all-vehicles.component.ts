import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../Services/vehicles.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './all-vehicles.component.html',
  styleUrls: ['./all-vehicles.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles: any[] = [];
  drivers: any[] = [];

  constructor(private vehicleService: VehicleService, private router: Router) { }

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

  //loadDrivers(): void {
  //  this.vehicleService.getAllDrivers().subscribe((data: any) => {
  //    console.log('API Response:', data);
  //    if (data && data['Drivers']) {
  //      this.drivers = data['Drivers'];
  //      console.log('drivers Array:', this.drivers);
  //    } else {
  //      console.error('No drivers found in the response');
  //    }
  //  }, (error: any) => {
  //    console.error('Error fetching drivers:', error);
  //  });
  //}

  showMore(id: number): void {
    this.router.navigate(['/vehicle', id]);

  }
}
