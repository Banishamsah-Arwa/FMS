import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GeofencesService {
  private apiUrl = 'http://localhost:5192/geofences';

  constructor(private http: HttpClient) { }

  getGeofences(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
