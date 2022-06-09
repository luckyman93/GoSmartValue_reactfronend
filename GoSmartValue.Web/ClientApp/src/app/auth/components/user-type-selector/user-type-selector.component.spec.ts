import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTypeSelectorComponent } from './user-type-selector.component';

describe('UserTypeSelectorComponent', () => {
  let component: UserTypeSelectorComponent;
  let fixture: ComponentFixture<UserTypeSelectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserTypeSelectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserTypeSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
