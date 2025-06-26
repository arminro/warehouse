import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseHome } from './warehouse-home';

describe('WarehouseHome', () => {
  let component: WarehouseHome;
  let fixture: ComponentFixture<WarehouseHome>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WarehouseHome]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WarehouseHome);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
