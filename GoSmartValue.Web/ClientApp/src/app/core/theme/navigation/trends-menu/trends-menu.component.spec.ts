import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrendsMenuComponent } from './trends-menu.component';

describe('TrendsMenuComponent', () => {
  let component: TrendsMenuComponent;
  let fixture: ComponentFixture<TrendsMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrendsMenuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrendsMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
