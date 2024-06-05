import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RouteHistoryService {
  private baseUrl = 'http://localhost:5192/routeHistory';

  constructor(private http: HttpClient) { }

  
  addRouteHistory(routeHistory: any): Observable<any> {
    
    const GvarRouteHistory = {
      
        DicOfDic: {
          Tags: {
            VehicleID: routeHistory.vehicleID,
            VehicleDirection: routeHistory.vehicleDirection,
            Status: routeHistory.status,
            VehicleSpeed: routeHistory.vehicleSpeed,
            Epoch: routeHistory.epoch,
            Address: routeHistory.address,
            Longitude: routeHistory.latitude,
            Latitude: routeHistory.longitude

          }
        }
      
    };
    return this.http.post<any>(`${this.baseUrl}`, GvarRouteHistory);
  }

  getRouteHistoryId(vehicleID:number ): Observable<any> {

    return this.http.get<any>(`${this.baseUrl}/${vehicleID}`);

  }
}
