import { Observable } from "rxjs";
import { CollectionDto } from "../../models/collection-dto";

export interface IReadonlyDataService<TEntity> {
  getElements(pageNumber: number, pageSize: number): Observable<CollectionDto<TEntity>>;
}