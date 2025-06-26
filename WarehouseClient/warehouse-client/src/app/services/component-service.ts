import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ComponentModel } from '../models/component-model';
import { ServiceBase } from './service-base';

@Injectable({
  providedIn: 'root'
})
export class ComponentService extends ServiceBase {

  constructor(http: HttpClient) {
    super(http);
  }

  createElement(entity: ComponentModel) {
    console.log(entity);
      return this.http.post("http://localhost:5144/api/WarehouseManagement/component/new", JSON.stringify(entity), this.getHeaders());
    }
  
  
    deleteElement(id: number) {
      return this.http.delete(`http://localhost:5144/api/WarehouseManagement/component/delete?id=${id}`);
    }
}
