//// src/app/add-driver/add-driver.component.ts
//import { Component, NgModule } from '@angular/core';
//import { DriversService } from '../Services/driver.service';
//import { NgForm } from '@angular/forms';

//@Component({
//  selector: 'app-add-driver',
//  templateUrl: './driver-options.component.html',
//  styleUrls: ['./driver-option.component.css']
//})

//export class DriverOptionComponent {
//  driver = {driverID:'', driverName: '', phoneNumber: '' };
//  drivers: any[] = [];
//  constructor(private driverService: DriversService) { }
//  ngOnInit(): void {
//    this.loadDrivers();
//  }

//  updateDriver(inputs: any) {
//    this.driver.driverID = inputs.driverID;
//    this.driver.driverName = inputs.driverName;
//    this.driver.phoneNumber = inputs.phoneNumber;

//    this.driverService.updateDriver(this.driver).subscribe(response => {
//      console.log('Driver updated successfully', response);
//    }, error => {
//      console.error('Error updating driver', error);
//    });
//  }

//  deleteDriver(inputs: any) {

//  }
//  addDriver(inputs: any) {
//    this.driver.driverName = inputs.driverName;
//    this.driver.phoneNumber = inputs.phoneNumber;

//    this.driverService.addDriver(this.driver).subscribe(response => {
//      console.log('Driver added successfully', response);
//    }, error => {
//      console.error('Error adding driver', error);
//    });
//  }

//  loadDrivers(): void {
//    this.driverService.getAllDrivers().subscribe(
//      (data: any) => {
//        if (data && data['Drivers']) {
//          this.drivers = data.Drivers;
//          console.log('Drivers Array:', this.drivers);
//        } else {
//          console.error('No drivers found in the response');
//        }
//      },
//      error => {
//        console.error('Error fetching drivers:', error);
//      }
//    );
//  }


//}


// src/app/add-driver/add-driver.component.ts
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DriversService } from '../Services/driver.service';

@Component({
  selector: 'app-add-driver',
  templateUrl: './driver-options.component.html',
  styleUrls: ['./driver-option.component.css']
})
export class DriverOptionComponent {
  driver = { driverID: '', driverName: '', phoneNumber: '' };
  drivers: any[] = [];
  constructor(private driverService: DriversService) { }
  ngOnInit(): void {
    this.loadDrivers();
  }

  updateDriver(form: NgForm) {
    const inputs = form.value;
    this.driver.driverID = inputs.driverID;
    this.driver.driverName = inputs.updatedDriverName;
    this.driver.phoneNumber = inputs.updatedPhoneNumber;

    this.driverService.updateDriver(this.driver).subscribe(response => {
      console.log('Driver updated successfully', response);
    }, error => {
      console.error('Error updating driver', error);
    });
  }

  deleteDriver(form: NgForm) {
    const inputs = form.value;
     this.driver.driverID = inputs.driverIdToDelete;

    this.driverService.deleteDriver(this.driver).subscribe(response => {
      console.log('Driver deleted successfully', response);
    }, error => {
      console.error('Error deleting driver', error);
    });
  }

  addDriver(form: NgForm) {
    const inputs = form.value;
    this.driver.driverName = inputs.driverName;
    this.driver.phoneNumber = inputs.phoneNumber;

    this.driverService.addDriver(this.driver).subscribe(response => {
      console.log('Driver added successfully', response);
    }, error => {
      console.error('Error adding driver', error);
    });
  }

  loadDrivers(): void {
    this.driverService.getAllDrivers().subscribe(
      (data: any) => {
        if (data && data['Drivers']) {
          this.drivers = data.Drivers;
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
}
