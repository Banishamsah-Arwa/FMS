import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InformationService {
  private baseUrl = 'http://localhost:5192/vehicleInformation';


  constructor(private http: HttpClient) { }


  addInformation(information: any) {
    const vehicleInformation = {
      DicOfDic: {
        Tags: {
          VehicleID: information.VehicleID,
          DriverID: information.DriverID,
          VehicleMake: information.VehicleMake,
          VehicleModel: information.VehicleModel,
          PurchaseDate: information.PurchaseDate
        }
      }
    };

    return this.http.post<any>(this.baseUrl, vehicleInformation);

  }

  updateInformation(information: any): Observable<any> {
    const vehicleInformation = {
      DicOfDic: {
        Tags: {
          VehicleID: information.VehicleID,
          DriverID: information.DriverID,
          VehicleMake: information.VehicleMake,
          VehicleModel: information.VehicleModel,
          PurchaseDate: information.PurchaseDate
        }
      }
    };

    return this.http.put<any>(this.baseUrl, vehicleInformation);
  }

  updateInformationDriverID(information: any): Observable<any> {
    const vehicleInformation = {
      DicOfDic: {
        Tags: {
          VehicleID: information.VehicleID,
          DriverName: information.DriverName,
         
        }
      }
    };

    return this.http.put<any>(`${this.baseUrl}/updateDriverID`, vehicleInformation);
  }
  deleteInformation(information: any): Observable<any> {
    const vehicleInformation = {
      body: {
        DicOfDic: {
          Tags: {
            VehicleID: information.VehicleID
          }
        }
      }
    };
    return this.http.delete<any>(this.baseUrl, vehicleInformation);
  }

}
