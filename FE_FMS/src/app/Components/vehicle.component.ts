import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { VehicleService } from '../Services/vehicles.service';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css']
})
export class VehicleComponent {
  vehicle = { vehicleNumber: '', vehicleType: '', vehicleID:'' };

  constructor(private vehicleService: VehicleService) { }

  addVehicleInformation(form: NgForm) {
    this.vehicle.vehicleNumber = form.value.vehicleNumber;
    this.vehicle.vehicleType = form.value.vehicleType;
    this.vehicleService.addVehicle(this.vehicle).subscribe(
      response => {
        console.log('Vehicle information added successfully:', response);
        form.reset();
      },
      error => {
        console.error('Error occurred while adding vehicle information:', error);
      }
    );
  }

  updateVehicleInformation(form: NgForm) {
    this.vehicle.vehicleID = form.value.vehicleID;
    this.vehicle.vehicleNumber = form.value.updatedVehicleNumber;
    this.vehicle.vehicleType = form.value.updatedVehicleType;
    this.vehicleService.updateVehicle(this.vehicle).subscribe(
      response => {
        console.log('Vehicle information updated successfully:', response);
        form.reset();
      },
      error => {
        console.error('Error occurred while updating vehicle information:', error);
      }
    );
  }

  deleteVehicleInformation(form: NgForm) {
    this.vehicle.vehicleID = form.value.vehicleID;
    this.vehicleService.deleteVehicle(this.vehicle).subscribe(
      response => {
        console.log('Vehicle information deleted successfully:', response);
        form.reset();
      },
      error => {
        console.error('Error occurred while deleting vehicle information:', error);
      }
    );
  }
}
