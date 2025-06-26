import { Injectable } from '@angular/core';
import { ServiceBase } from './service-base';
import { ComponentType } from '../models/component-type';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppConfig } from '../configuration/app-config';
import { IDataService } from './interfaces/idata-service';
import { IAppConfig } from '../configuration/interfaces/iapp-config';
import { Observable } from 'rxjs';
import { IReadonlyDataService } from './interfaces/ireadonly-data-service';
import { CollectionDto } from '../models/collection-dto';

@Injectable({
  providedIn: 'root'
})
export abstract class ComponentTypeService extends ServiceBase implements IReadonlyDataService<ComponentType> {
    //private settings: IAppConfig;

    constructor(http: HttpClient) {
      super(http);
    } 

    
// TODO: add the urls
 createElement(entity: ComponentType) {
    return this.http.post<ComponentType>("http://localhost:5144/api/warehousemanagement/component-type/new", JSON.stringify(entity), this.getHeaders());
  }

  updateElement(entity: ComponentType): Observable<ComponentType> {
        console.log(entity);
    return this.http.put<ComponentType>("http://localhost:5144/api/WarehouseManagement/component-type/change", JSON.stringify(entity), this.getHeaders());
  }

  deleteElement(id: number) {
      return this.http.delete(`http://localhost:5144/api/warehouseManagement/component-type/delete?id=${id}`);
  }

  getElements(pageNumber: number, pageSize: number): Observable<CollectionDto<ComponentType>> {
    return this.http.get<CollectionDto<ComponentType>>(`http://localhost:5144/api/warehousemanagement/all?pagenumber=${pageNumber}&pageSize=${pageSize}`);
  }
}