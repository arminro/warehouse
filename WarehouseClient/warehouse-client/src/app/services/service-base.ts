import { Injectable } from '@angular/core';
import { IDataService } from './interfaces/idata-service';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppConfig } from '../configuration/app-config';
import { IAppConfig } from '../configuration/interfaces/iapp-config';

@Injectable({
  providedIn: 'root'
})
export abstract class ServiceBase {

    constructor(protected readonly http: HttpClient) {
    }
    
    getHeaders() {
    return {
      headers: new HttpHeaders()
      .set('Content-Type', 'application/json')
    };
  }
}
