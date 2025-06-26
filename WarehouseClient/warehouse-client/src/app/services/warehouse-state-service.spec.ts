import { TestBed } from '@angular/core/testing';

import { WarehouseStateService } from './warehouse-state-service';

describe('WarehouseStateService', () => {
  let service: WarehouseStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WarehouseStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
