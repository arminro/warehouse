import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseStatistics } from './warehouse-statistics';

describe('WarehouseStatistics', () => {
  let component: WarehouseStatistics;
  let fixture: ComponentFixture<WarehouseStatistics>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WarehouseStatistics]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WarehouseStatistics);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
