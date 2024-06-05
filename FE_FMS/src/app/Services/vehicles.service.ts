import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private vehicleUrl = 'http://localhost:5192/vehicles';

  private baseUrl = 'http://localhost:5192/details';
  private allDriversUrl = 'http://localhost:5192/drivers';

  constructor(private http: HttpClient) { }

  getAllVehicles(): Observable<any> {

   return this.http.get<any>(`${this.baseUrl}`);

  }

 getAllDrivers(): Observable<any> {

    return this.http.get<any>(`${this.allDriversUrl}`);

  }

  getVehicleDetails(vehicleID: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${vehicleID}`);
  }


  addVehicle(vehicle: any) {
    const vehicleGvar = {
      DicOfDic: {
        Tags: {
          VehicleNumber: vehicle.vehicleNumber,
          VehicleType: vehicle.vehicleType,
        }
      }
    };

    return this.http.post<any>(this.vehicleUrl, vehicleGvar);

  }

  updateVehicle(vehicle: any): Observable<any> {
    const vehicleGvar = {
      DicOfDic: {
        Tags: {
          VehicleID: vehicle.vehicleID,
          VehicleNumber: vehicle.vehicleNumber,
          VehicleType: vehicle.vehicleType,
        }
      }
    };

    return this.http.put<any>(this.vehicleUrl, vehicleGvar);
  }


  deleteVehicle(vehicle: any): Observable<any> {
    const vehicleGvar = {
      body: {
        DicOfDic: {
          Tags: {
            VehicleID: vehicle.vehicleID
          }
        }
      }
    };
    return this.http.delete<any>(this.vehicleUrl, vehicleGvar);
  }
}
