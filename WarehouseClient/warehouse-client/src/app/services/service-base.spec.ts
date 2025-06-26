import { TestBed } from '@angular/core/testing';

import { ServiceBase } from './service-base';

describe('ServiceBase', () => {
  let service: ServiceBase;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceBase);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
