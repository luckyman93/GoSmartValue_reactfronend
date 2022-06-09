import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewInstructionsComponent } from './view-instructions.component';

describe('ViewInstructionsComponent', () => {
  let component: ViewInstructionsComponent;
  let fixture: ComponentFixture<ViewInstructionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewInstructionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewInstructionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
