import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompletedInstructionsComponent } from './completed-instructions.component';

describe('CompletedInstructionsComponent', () => {
  let component: CompletedInstructionsComponent;
  let fixture: ComponentFixture<CompletedInstructionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompletedInstructionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompletedInstructionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
