import { ComponentType } from "./component-type";


export class WarehouseStatisticsModel {
    totalMassInGrams: number;
    totalValueInHungarianForints: number;
    totalValueInEuros: number;
    heaviestProduct: ComponentType
    productWithLargestSum : ComponentType;

     constructor(data?: Partial<WarehouseStatisticsModel>) {
        this.totalMassInGrams= data?.totalMassInGrams ?? 0;
        this.totalValueInHungarianForints = data?.totalValueInHungarianForints ?? 0;
        this.totalValueInEuros= data?.totalValueInEuros ?? 0;
        this.heaviestProduct = data?.heaviestProduct ?? new ComponentType();
        this.productWithLargestSum = data?.productWithLargestSum ?? new ComponentType();
  }
}
