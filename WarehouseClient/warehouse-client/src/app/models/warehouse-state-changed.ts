import { ComponentModel } from "./component-model";
import { ComponentType } from "./component-type";

export class WarehouseStateChanged {
    changeTimestamp: Date;
    componentChanged?: ComponentModel;
    componentTypeChanged?: ComponentType;

  constructor(data?: Partial<WarehouseStateChanged>) {
    this.changeTimestamp = data?.changeTimestamp ? new Date(data.changeTimestamp) : new Date();
    this.componentChanged = data?.componentChanged ?? new ComponentModel()
    this.componentTypeChanged = data?.componentTypeChanged ?? new ComponentType()
  }
}
