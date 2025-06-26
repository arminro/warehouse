import { ComponentModel } from "./component-model";

export class ComponentType {
  id: number
  name: string;
  priceInHungarianForints: number;
  priceInEuros?: number;
  description: string;
  massInGrams: number;
  components?: ComponentModel[];

  constructor(data?: Partial<ComponentType>) {
    this.id = data?.id ?? 0;
    this.name = data?.name ?? '';
    this.priceInHungarianForints = data?.priceInHungarianForints ?? 0;
    this.priceInEuros = data?.priceInEuros ?? 0;
    this.description = data?.description ?? '';
    this.massInGrams = data?.massInGrams ?? 0;

    this.components = (data?.components ?? []).map(c => new ComponentModel(c));
  }
}
