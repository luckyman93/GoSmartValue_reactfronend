import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ValuationsComponent } from './valuations.component';

describe('ValuationsComponent', () => {
  let component: ValuationsComponent;
  let fixture: ComponentFixture<ValuationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ValuationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ValuationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
