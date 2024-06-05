import { Component, OnInit } from '@angular/core';
import { GeofencesService } from '../Services/geofences.service';

@Component({
  selector: 'app-geofences',
  templateUrl: './geofences.component.html',
  styleUrls: ['./geofences.component.css']
})
export class GeofencesComponent implements OnInit {
  geofences!: any[];

  constructor(private geofencesService: GeofencesService) { }

  ngOnInit(): void {
    this.fetchGeofences();
  }

  fetchGeofences(): void {
   

    console.log('Loading geofences');
    this.geofencesService.getGeofences().subscribe((data: any) => {
      console.log('API Response:', data);
      if (data && data['Geofences']) {
        this.geofences = data['Geofences'];
        console.log('Geofences Array:', this.geofences);
      } else {
        console.error('No Geofences found in the response');
      }
    }, (error: any) => {
      console.error('Error fetching Geofences:', error);
    });
  }
}
