import { TestBed } from '@angular/core/testing';

import { WarehouseStatisticsService } from './warehouse-statistics-service';

describe('WarehouseStatisticsService', () => {
  let service: WarehouseStatisticsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WarehouseStatisticsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
