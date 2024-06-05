

import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DriversService } from '../Services/driver.service';
import { InformationService } from '../Services/vehicleinformation.service';

@Component({
  selector: 'app-information',
  templateUrl: './Information.component.html',
  styleUrls: ['./Information.component.css']
})
export class InformationComponent {

  information = { VehicleID: '', DriverID: '', VehicleMake: '', VehicleModel: '', PurchaseDate: '' };
  constructor(private informationService: InformationService) { }
  ngOnInit(): void {
  }

  addVehicleInformation(form: NgForm) {
    const inputs = form.value;
    const purchaseDateLocal = this.convertToDateTimeLocal(inputs.purchaseDate);

    this.information.VehicleID = inputs.vehicleID;
    this.information.DriverID = inputs.driverID;
    this.information.VehicleMake = inputs.vehicleMake;
    this.information.VehicleModel = inputs.vehicleModel;
    this.information.PurchaseDate = this.convertToDateTimeOffset(purchaseDateLocal);;

    this.informationService.addInformation(this.information).subscribe(response => {
      console.log('vehicle Information added successfully', response);
    }, error => {
      console.error('Error adding vehicle information', error);
    });
  }

  convertToDateTimeOffset(localDateTime: string): string {
    const date = new Date(localDateTime);
    const isoString = date.toISOString();
    return isoString.slice(0, -5) + 'Z';
  }

  convertToDateTimeLocal(isoDate: string): string {
    const withoutMilliseconds = isoDate.split('.')[0];
    const date = new Date(withoutMilliseconds);
    const pad = (n: number) => n.toString().padStart(2, '0');
    const year = date.getFullYear();
    const month = pad(date.getMonth() + 1);
    const day = pad(date.getDate());
    const hours = pad(date.getHours());
    const minutes = pad(date.getMinutes());
    const seconds = pad(date.getSeconds()); 
    const milliseconds = date.getUTCMilliseconds().toString().padStart(3, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }

  deleteVehicleInformation(form: NgForm) {
    const inputs = form.value;
    this.information.VehicleID = inputs.vehicleID;

    this.informationService.deleteInformation(this.information).subscribe(response => {
      console.log('informationService deleted successfully', response);
    }, error => {
      console.error('Error deleting informationService', error);
    });
  }

  updateVehicleInformation(form: NgForm) {
    const inputs = form.value;
    const purchaseDateLocal = this.convertToDateTimeLocal(inputs.purchaseDate);

    this.information.VehicleID = inputs.updatedVehicleID;
    this.information.DriverID = inputs.updatedDriverID;
    this.information.VehicleMake = inputs.updatedVehicleMake;
    this.information.VehicleModel = inputs.updatedVehicleModel;
    this.information.PurchaseDate = this.convertToDateTimeOffset(purchaseDateLocal);;

    this.informationService.updateInformation(this.information).subscribe(response => {
      console.log('vehicle Information updated successfully', response);
    }, error => {
      console.error('Error updating vehicle information', error);
    });
  }

 
}
