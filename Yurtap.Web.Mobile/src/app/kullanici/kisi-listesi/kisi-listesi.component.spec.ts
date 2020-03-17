import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KisiListesiComponent } from './kisi-listesi.component';

describe('KullaniciListesiComponent', () => {
  let component: KisiListesiComponent;
  let fixture: ComponentFixture<KisiListesiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KisiListesiComponent ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KisiListesiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
