export class CollectionDto<Payload> {
    payload: Payload[];  
    totalNumber: number;

    constructor(data?: Partial<CollectionDto<Payload>>) {
        this.payload = data?.payload ?? [];
        this.totalNumber = data?.totalNumber ?? 0;
    }
}
