/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { YapimAsamasindaComponent } from './yapimAsamasinda.component';

describe('YapimAsamasindaComponent', () => {
  let component: YapimAsamasindaComponent;
  let fixture: ComponentFixture<YapimAsamasindaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YapimAsamasindaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YapimAsamasindaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
