import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DriversService {
  private baseUrl = 'http://localhost:5192/drivers';
 

  constructor(private http: HttpClient) { }

 

  getAllDrivers(): Observable<any> {

    return this.http.get<any>(`${this.baseUrl}`);

  }

  addDriver(driver: any) {
    const dataToSend = {
      DicOfDic: {
        Tags: {
          DriverName: driver.driverName,
          PhoneNumber: driver.phoneNumber
        }
      }
    };

    return this.http.post<any>(this.baseUrl, dataToSend);

  }

  updateDriver( driver: any): Observable<any> {
    const driverGvar= {
      DicOfDic: {
        Tags: {
          DriverID: driver.driverID,
          DriverName: driver.driverName,
          PhoneNumber: driver.phoneNumber
        }
      }
    };

    return this.http.put<any>(this.baseUrl, driverGvar);
  }

 
  deleteDriver( driver: any): Observable<any> {
    const options = {
      body: {
        DicOfDic: {
          Tags: {
            DriverID: driver.driverID
          }
        }
      }
    };
    return this.http.delete<any>(this.baseUrl, options);
  }

}
