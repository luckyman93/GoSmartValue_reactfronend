import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideNavTabContentComponent } from './side-nav-tab-content.component';

describe('SideNavTabContentComponent', () => {
  let component: SideNavTabContentComponent;
  let fixture: ComponentFixture<SideNavTabContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SideNavTabContentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SideNavTabContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
