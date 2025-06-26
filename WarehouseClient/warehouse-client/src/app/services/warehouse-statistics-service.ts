import { Injectable } from '@angular/core';
import { IReadonlyDataService } from './interfaces/ireadonly-data-service';
import { WarehouseStatisticsModel } from '../models/warehouse-statistics.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class WarehouseStatisticsService{

  constructor(private readonly http: HttpClient) { }

  getStatistics(): Observable<WarehouseStatisticsModel> {
    return this.http.get<WarehouseStatisticsModel>(`http://localhost:5144/api/warehousemanagement/stat`);
  }
}
