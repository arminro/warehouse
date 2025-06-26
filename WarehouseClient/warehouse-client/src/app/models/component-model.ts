export class ComponentModel {
    catalogId: string;
    componentId: number;
    componentTypeId: number;

     constructor(data?: Partial<ComponentModel>) {
        this.catalogId = data?.catalogId ?? '';
        this.componentId = data?.componentId ?? 0;
        this.componentTypeId = data?.componentTypeId ?? 0;
  }
}
