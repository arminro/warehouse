import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { WarehouseStateChanged } from '../models/warehouse-state-changed';
import { IReadonlyDataService } from './interfaces/ireadonly-data-service';
import { Observable } from 'rxjs';
import { CollectionDto } from '../models/collection-dto';

@Injectable({
  providedIn: 'root'
})
export class WarehouseStateService {

     constructor(private readonly http: HttpClient) {
    } 
  getIncomingElements(pageNumber: number, pageSize: number): Observable<CollectionDto<WarehouseStateChanged>> {
    return this.http.get<CollectionDto<WarehouseStateChanged>>(`http://localhost:5144/api/WarehouseManagement/incoming?pagenumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getOutgoingElements(pageNumber: number, pageSize: number): Observable<CollectionDto<WarehouseStateChanged>> {
    return this.http.get<CollectionDto<WarehouseStateChanged>>(`http://localhost:5144/api/WarehouseManagement/outgoing?pagenumber=${pageNumber}&pageSize=${pageSize}`);
  }
}
