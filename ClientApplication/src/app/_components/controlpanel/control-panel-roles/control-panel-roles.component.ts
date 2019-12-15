import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '@core/_services/alert.service';
import { RoleService } from '@core/_services/role.service';

@Component({
  selector: 'app-control-panel-roles',
  templateUrl: './control-panel-roles.component.html',
  styleUrls: ['./control-panel-roles.component.css']
})
export class ControlPanelRolesComponent implements OnInit {

  constructor() { }

  ngOnInit() { }
}
