import { Observable } from "rxjs";

export interface IDataService<TEntity> {

  createElement(entity: TEntity): void;

  updateElement(entity: TEntity) : Observable<TEntity>;

  deleteElement(id: number): Observable<TEntity>;
}
