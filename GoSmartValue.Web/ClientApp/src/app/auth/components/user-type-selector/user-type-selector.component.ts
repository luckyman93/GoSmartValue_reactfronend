import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-user-type-selector',
  templateUrl: './user-type-selector.component.html',
  styleUrls: ['./user-type-selector.component.scss']
})
export class UserTypeSelectorComponent implements OnInit {

  @Input() displayModal: boolean = true;
  @Output() selectedUserType = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  addItem(display: boolean) {
    this.displayModal = display;
  }

  selectUser(userType:string) {
    this.displayModal = false;
    this.selectedUserType.emit(userType);
  }
}
