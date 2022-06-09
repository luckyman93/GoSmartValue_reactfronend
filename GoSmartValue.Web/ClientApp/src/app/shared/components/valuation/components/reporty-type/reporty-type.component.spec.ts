import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportyTypeComponent } from './reporty-type.component';

describe('ReportyTypeComponent', () => {
  let component: ReportyTypeComponent;
  let fixture: ComponentFixture<ReportyTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportyTypeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportyTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
