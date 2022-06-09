import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RejectedInstructionsComponent } from './rejected-instructions.component';

describe('RejectedInstructionsComponent', () => {
  let component: RejectedInstructionsComponent;
  let fixture: ComponentFixture<RejectedInstructionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RejectedInstructionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RejectedInstructionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
