import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VehicleService } from '../Services/vehicles.service';
import { DriversService } from '../Services/driver.service';
import { InformationService } from '../Services/vehicleinformation.service';

@Component({
  selector: 'app-vehicle-detail',
  templateUrl: './vehicle-detail.component.html',
  styleUrls: ['./vehicle-detail.component.css']
})
export class VehicleDetailComponent implements OnInit {
  vehicleId!: number;
  vehicleInformation!: any;
  drivers: any[] = [];
  selectedDriver: any; 
  info = { VehicleID: '', DriverName: '' };
  driverName: any;
  constructor(
    private route: ActivatedRoute,
    private vehicleService: VehicleService,
    private driverService: DriversService,
    private vehicleinformation: InformationService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const idParam = params.get('id');
      if (idParam) {
        this.vehicleId = +idParam;
        this.loadVehicleDetails(this.vehicleId);
      } else {
        console.error('Vehicle ID parameter is missing');
      }
      this.loadDrivers();
    });
  }

  loadVehicleDetails(id: number): void {
    this.vehicleService.getVehicleDetails(id).subscribe(
      (data: any) => {
        console.log(data);
        if (data && data['Spicific Vehicle Information'] && data['Spicific Vehicle Information'].length > 0) {
          this.vehicleInformation = data['Spicific Vehicle Information'][0];
          console.log('Vehicle Info:', data['Specific Vehicle Information'][0]);
        } else {
          console.error('No vehicle information found in the response');
        }
      },
      error => {
        console.error('Error fetching vehicle information:', error);
      }
    );
  }

  loadDrivers(): void {
    this.driverService.getAllDrivers().subscribe(
      (data: any) => {
        if (data && data['Drivers']) {
          this.drivers = data['Drivers'];
          console.log('Drivers Array:', this.drivers);
        } else {
          console.error('No drivers found in the response');
        }
      },
      error => {
        console.error('Error fetching drivers:', error);
      }
    );
  }

  onDriverSelected(selectedDriverName: string): void {
    console.log('Selected Driver:', selectedDriverName);
    this.selectedDriver = this.drivers.find(driver => driver.driverName === selectedDriverName);
    if (this.selectedDriver) {
      this. driverName = this.selectedDriver.driverName;
      console.log(this.driverName); 
    } else {
      console.error('Driver not found');
    }    this.info.VehicleID = `${this.vehicleId}`;
    this.info.DriverName = this.driverName;
    this.vehicleinformation.updateInformationDriverID(this.info).subscribe(
      response => {
        console.log('Vehicle information updated successfully:', response);
      },
      error => {
        console.error('Error occurred while updating vehicle information:', error);
      }
    );

  }
}
