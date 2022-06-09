import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ValuationsMenuComponent } from './valuations-menu.component';

describe('ValuationsMenuComponent', () => {
  let component: ValuationsMenuComponent;
  let fixture: ComponentFixture<ValuationsMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ValuationsMenuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ValuationsMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
